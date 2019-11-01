using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppG2.Model;

namespace AppG2.Controller
{
    class ContactService
    {
        public static List<Contact> GetContacts(string pathDataFile)
        {
            if (File.Exists(pathDataFile))
            {
                List<Contact> contacts = new List<Contact>();
                var listLines = File.ReadAllLines(pathDataFile);
                foreach (var line in listLines)
                {
                    var rs = line.Split(new char[] { '#' });
                    Contact contact = new Contact
                    {
                        ID = rs[0],
                        ContactName = rs[1],
                        ContactPhone = rs[2],
                        Email = rs[3]
                    };
                    contacts.Add(contact);
                }
                return contacts;
            }
            else
            {
                return null;
            }
        }

        public static List<Contact> SearchContacts(string pathDataFile, string value)
        {
            if (File.Exists(pathDataFile))
            {
                List<Contact> contacts = new List<Contact>();
                var listLines = File.ReadAllLines(pathDataFile);
                foreach (var line in listLines)
                {
                    var rs = line.Split(new char[] { '#' });
                    var name = rs[1].ToLower();
                    if (name.Contains(value.ToLower())) {
                        Contact contact = new Contact
                        {
                            ID = rs[0],
                            ContactName = rs[1],
                            ContactPhone = rs[2],
                            Email = rs[3]
                        };
                        contacts.Add(contact);
                    }
                }
                return contacts;
            }
            else
            {
                return null;
            }
        }

        public static void deleteContact(string pathDataFile, string contactID)
        {
            if (File.Exists(pathDataFile))
            {
                var listContacts = File.ReadAllLines(pathDataFile);
                File.WriteAllText(pathDataFile, "");
                foreach (var lineContact in listContacts)
                {
                    var rsContact = lineContact.Split(new char[] { '#' });
                    if (rsContact[0] != contactID)
                    {
                        File.AppendAllText(pathDataFile, lineContact + "\r\n");
                    }
                }

            }
        }

        public static void addContact(string contactName, string contactPhone, string email, string pathDataFile)
        {
            var IDContact = Guid.NewGuid().ToString();
            string lineContact = IDContact + "#" + contactName + "#" + contactPhone + "#" + email;
            if (File.Exists(pathDataFile))
            {
                File.AppendAllText(pathDataFile, "\r\n" + lineContact + "\n");
            }
        }

        public static void editContact(string contactID, string contactName, string contactPhone, string email, string pathDataFile)
        {
            if (File.Exists(pathDataFile))
            {
                var listContactLines = File.ReadAllLines(pathDataFile);
                File.WriteAllText(pathDataFile, "");
                bool isNextLine = false;
                var rsLastLine = listContactLines[listContactLines.Length - 1].Split(new char[] { '#' });
                foreach (var lineContact in listContactLines)
                {
                    var rsContact = lineContact.Split(new char[] { '#' });
                    if (rsContact[0] != contactID)
                    {
                        if(rsLastLine[0] == rsContact[0])
                        {
                            File.AppendAllText(pathDataFile, lineContact);
                        }
                        else
                        {
                            File.AppendAllText(pathDataFile, lineContact + "\r\n");
                        }
                         
                    }
                    else
                    {
                        string content = contactID + "#" + contactName + "#" + contactPhone + "#" + email;
                        if (rsLastLine[0] == rsContact[0])
                        {
                            File.AppendAllText(pathDataFile, content);
                        }
                        else
                        {
                            File.AppendAllText(pathDataFile, content + "\r\n");
                        }
                        isNextLine = true;
                    }
                }

            }
        }
    }
}
