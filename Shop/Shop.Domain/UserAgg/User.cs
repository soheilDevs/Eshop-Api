using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using System.Net;
using Shop.Domain.UserAgg.Services;
using System;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{
    public User(string name, string family, string phoneNumber, string email, string password, Gender gender, IDomainUserService domainService)
    {
        Guard(phoneNumber, email, domainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
    }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Gender Gender { get; private set; }
    public List<UserRole> Roles { get; private set; }
    public List<Wallet> Wallets { get; private set; }
    public List<UserAddress> Addresses { get; private set; }

    public void Edit(string name, string family, string phoneNumber, string email, Gender gender, IDomainUserService domainService)
    {
        Guard(phoneNumber, email, domainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public static User RegisterUser(string email, string phoneNumber, string password, IDomainUserService domainService)   //mishe bejaye email phoneNumber ham gereft ke register anjam beshe
    {
        return new User("", "", phoneNumber, email, password, Gender.None, domainService);
    }

    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }
    public void DeleteAddress(long addressId)
    {
        var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (oldAddress == null)
            throw new NullOrEmptyDomainDataException("Address Not Found");

        Addresses.Remove(oldAddress);
    }
    public void EditAddress(UserAddress address)
    {
        var oldAddress = Addresses.FirstOrDefault(f => f.Id == address.Id);
        if (oldAddress == null)
            throw new NullOrEmptyDomainDataException("Address Not Found");
        Addresses.Remove(oldAddress);
        Addresses.Add(address);
    }

    public void ChargeWallet(Wallet wallet)
    {
        Wallets.Add(wallet);
    }

    public void SetRoles(List<UserRole> roles)
    {
        roles.ForEach(f=>f.UserId=Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }

    public void Guard(string phoneNumber, string email, IDomainUserService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));
        if (phoneNumber.Length != 11)
            throw new InvalidDomainDataException("شماره موبایل نا معتبر است");

        if (email.IsValidEmail() == false)
            throw new InvalidDomainDataException("ایمیل نا معتبر است");

        if (phoneNumber != PhoneNumber)
            if (domainService.IsPhoneNumberExist(phoneNumber) == true)
                throw new InvalidDomainDataException("شماره موبایل تکراری است");

        if (email != Email)
            if (domainService.IsEmailExist(email) == true)
                throw new InvalidDomainDataException("ایمیل موبایل تکراری است");
    }
}