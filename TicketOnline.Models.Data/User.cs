using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Data
{
    public class User
    {
        private int _id;
        private string _screenName, _lastName, _firstName, _email, _passwd, _address, _token;
        private bool _isActive, _isAdmin;

        public int Id 
        { 
            get { return _id; } 
            set { _id = value; }
        }
        
        public string LastName 
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string FirstName 
        { 
            get { return _firstName; } 
            set { _firstName = value; } 
        }
        public string ScreenName
        {
            get { return _screenName; }
            set { _screenName = value; }
        }
        public string Email 
        { 
            get { return _email; } 
            set { _email = value; } 
        }
        public string Passwd 
        { 
            get { return _passwd; } 
            set { _passwd = value; } 
        }
        public string Address 
        { 
            get { return _address; } 
            set { _address = value; } 
        }
        public bool IsActive 
        { 
            get { return _isActive; } 
            set { _isActive = value; } 
        }
        public bool IsAdmin 
        { 
            get { return _isAdmin; } 
            set { _isAdmin = value; } 
        }

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public User(string screenName, string email)
        {
            ScreenName = screenName;
            Email = email;
        }

        public User(string screenName, string email, string passwd) 
            : this(screenName, email)
        {            
            Passwd = passwd;
        }

        public User(int id, string lastName, string firstName, string screenName, string email, string address)
            : this(screenName, email)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Address = address;
        }

        public User(string lastName, string firstName, string screenName, string email, string passwd, string address)
            : this(screenName, email, passwd)
        {
            LastName = lastName;
            FirstName = firstName;
            Address = address;
        }

        public User(int id, string lastName, string firstName, string screenName, string email, string passwd, string address, bool isActive, bool isAdmin) 
            : this (lastName, firstName, screenName, email, passwd, address)
        {
            Id = id;
            IsActive = isActive;
            IsAdmin = isAdmin;
        }

        public User(int id, string lastName, string firstName, string screenName, string email, string passwd, string address, bool isActive, bool isAdmin, string token)
            : this(id,lastName, firstName, screenName, email, passwd, address, isActive, isAdmin)
        {
            Token = token;
        }
    }
}
