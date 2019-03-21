using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AddressBook.Controllers
{
    public class AddressController : ApiController
    {
        Entities db = new Entities();

        [HttpGet]
        [ActionName("GetContacts")]
        public HttpResponseMessage GetContacts(string search)
        {
            var contacts = db.Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                contacts = contacts.Where(a => a.FirstName.Contains(search) || a.LastName.Contains(search));

            return Request.CreateResponse(HttpStatusCode.OK, new { status=200, data = contacts.Select(a=>new {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Id = a.Id
            }).ToList()});
        }

        [HttpGet]
        [ActionName("SaveContact")]
        public HttpResponseMessage SaveContact(string FirstName, string LastName, int id=0)
        {
            if (id > 0)
            {
                var contact = db.Contacts.Find(id);
                contact.FirstName = FirstName;
                contact.LastName = LastName;
                db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    status = 200,
                    id = contact.Id,
                    data = "Contact updated successfully!"
                });
            }
            else
            {
                var contact = new Contact
                {
                    FirstName = FirstName,
                    LastName = LastName
                };
                db.Contacts.Add(contact);

                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    status = 200,
                    id = contact.Id,
                    data = "Contact created successfully!"
                });
            }

        }

        [HttpGet]
        [ActionName("GetContactDetails")]
        public HttpResponseMessage GetContactDetails(int id)
        {
            var contact = db.Contacts.Find(id);

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                status = 200,
                firstName = contact.FirstName,
                lastName = contact.LastName,
                data = contact.ContactDetails.Select(a=>new {
                    Type = a.DetailsType,
                    Details = a.Details,
                    Id = a.Id
                }).ToList()
            });
        }

        [HttpGet]
        [ActionName("DeleteContactDetails")]
        public async Task<HttpResponseMessage> DeleteContactDetails(int id)
        {
            if (id > 0)
            {
                var contactDetails = db.ContactDetails.Find(id);
                db.ContactDetails.Remove(contactDetails);
                await db.SaveChangesAsync();
            }
          
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                status = 200,
                data = "Details deleted successfully!"
            });
        }

        [HttpGet]
        [ActionName("SaveContactDetails")]
        public async Task<HttpResponseMessage> SaveContactNumber(int id, int cid, string number, int type)
        {
            if (id > 0)
            {
                var contactDetails = db.ContactDetails.Find(id);
                contactDetails.Details = number;
                db.Entry(contactDetails).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    status = 200,
                    id = contactDetails.Id,
                    data = "Details updated successfully!"
                });
            }
            else
            {
                var contactDetails = new ContactDetail
                {
                    Details = number,
                    DetailsType = type,
                    ContactId = cid
                };
                db.ContactDetails.Add(contactDetails);

                await db.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    status = 200,
                    id = contactDetails.Id,
                    data = "Details saved successfully!"
                });
            }

        }
        [HttpGet]
        [ActionName("DeleteContact")]
        public async Task<HttpResponseMessage> DeleteContact(int id)
        {
            var contact = db.Contacts.Find(id);

            foreach (var details in contact.ContactDetails.ToList()) {
                db.ContactDetails.Remove(details);
            }
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                status=200,
                data = "Contact deleted successfully!"
            });
        }
    }
}
