﻿using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool CreateContact(ContactRegistrationForm contactRegistrationForm);
    bool UpdateContact(Contact contact);
    bool DeleteContact(Contact contact);
    IEnumerable<Contact> GetAllContacts();
    Contact? GetContactById(string id);
}



