using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppG2.Model;

namespace AppG2.Controller
{
    public class StudentService
    {
        /// <summary>
        /// Lay sinh vien theo ma sinh vien
        /// </summary>
        /// <param name="idStudent">Ma sinh vien</param>
        /// <returns>Sinh vien co ma tuong ung hoac null</returns>
        public static Student GetStudent(string idStudent)
        {
            Student student = new Student
            {
                IDStudent = idStudent,
                FirstName = "Phuc",
                LastName = "Ngo Hoang Duc",
                DOB = new DateTime(1997, 10, 3),
                POB = "Thua Thien Hue",
                Gender = GENDER.Male
            };
            student.ListHistoryLearning = new List<HistoryLearning>();
            for(int i = 0; i < 12; i++)
            {
                HistoryLearning historyLearning = new HistoryLearning
                {
                    IDHistoryLearning = i.ToString(),
                    YearFrom = 2006 + i,
                    YearEnd = 2007 + i,
                    Address = "Thua Thien Hue",
                    IDStudent = idStudent
                };
                student.ListHistoryLearning.Add(historyLearning);
            }
            return student;
        }

        /// <summary>
        /// Lay sinh vien theo ma sinh vien tu File
        /// </summary>
        /// <param name="pathDataFile">Duong dan toi file du lieu</param>
        /// <param name="idStudent">Ma sinh vien</param>
        /// <returns>Sinh vien co ma tuong ung hoac null</returns>
        public static Student GetStudent(string pathDataFile, string pathHistoryFile, string idStudent)
        {
            
            if(File.Exists(pathDataFile))
            {
                CultureInfo culture = CultureInfo.InvariantCulture; // dinh dang ngay theo dinh dang tu quy dinh
                var listLines = File.ReadAllLines(pathDataFile);
                foreach (var line in listLines)
                {
                    var rs = line.Split(new char[] { '#' });
                    Student student = new Student
                    {
                        IDStudent = rs[0],
                        LastName = rs[1],
                        FirstName = rs[2],
                        Gender = rs[3] == "Male" ? GENDER.Male : (rs[3] == "Female" ? GENDER.Female : GENDER.Other),
                        DOB = DateTime.ParseExact(rs[4], "yyyy-MM-dd", culture),
                        POB = rs[5]
                    };
                    if (student.IDStudent == idStudent)
                    {
                        #region Get History Learning
                        if (File.Exists(pathHistoryFile))
                        {
                            var listHistoryLines = File.ReadAllLines(pathHistoryFile);
                            student.ListHistoryLearning = new List<HistoryLearning>();
                            foreach (var lineHistory in listHistoryLines)
                            {
                                var rsHistory = lineHistory.Split(new char[] { '#' });
                                if (rsHistory[4] == idStudent)
                                {
                                    HistoryLearning historyLearning = new HistoryLearning
                                    {
                                        IDHistoryLearning = rsHistory[0],
                                        YearFrom = Int32.Parse(rsHistory[1]),
                                        YearEnd = Int32.Parse(rsHistory[2]),
                                        Address = rsHistory[3],
                                        IDStudent = rsHistory[4]
                                    };
                                    student.ListHistoryLearning.Add(historyLearning);
                                }

                            }
                        }
                        #endregion
                        return student;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }        
        }

        public static void CreateHistoryLearning(int yearFrom, int yearEnd, string address, string maSinhVien, string pathHistoryFile)
        {
            var IDHistoryLearning = Guid.NewGuid().ToString();
            string lineHistory = IDHistoryLearning + "#" + yearFrom + "#" + yearEnd + "#" + address + "#" + maSinhVien;
            if (File.Exists(pathHistoryFile))
            {
                    File.AppendAllText(pathHistoryFile, "\r\n" + lineHistory + "\r\n");
            }
        }

        public static void EditHistoryLearning(string historyID, int yearFrom, int yearEnd, string address, string pathHistoryFile)
        {
            if (File.Exists(pathHistoryFile))
            {
                var listHistoryLines = File.ReadAllLines(pathHistoryFile);
                File.WriteAllText(pathHistoryFile, "");
                bool isNextLine = false;
                foreach (var lineHistory in listHistoryLines)
                {
                    var rsHistory = lineHistory.Split(new char[] { '#' });
                    if (rsHistory[0] != historyID)
                    {
                        if(!isNextLine)
                        {
                            File.AppendAllText(pathHistoryFile, lineHistory + "\r\n");
                        }
                        else
                        {
                            File.AppendAllText("\r\n" + pathHistoryFile, lineHistory + "\r\n");
                            isNextLine = false;
                        }
                            
                    }
                    else
                    {
                        MessageBox.Show(address);
                        string content = historyID + "#" + yearFrom + "#" + yearEnd + "#" + address + "#" + rsHistory[4];
                        File.AppendAllText(pathHistoryFile, content);
                        isNextLine = true;
                    }
                }

            }
        }

        public static void DeleteHistoryLearning(string pathHistoryFile, string historyID)
        {
            if (File.Exists(pathHistoryFile))
            {
                var listHistoryLines = File.ReadAllLines(pathHistoryFile);
                File.WriteAllText(pathHistoryFile, "");
                foreach (var lineHistory in listHistoryLines)
                {
                    var rsHistory = lineHistory.Split(new char[] { '#' });
                    if (rsHistory[0] != historyID)
                    {

                        File.AppendAllText(pathHistoryFile, lineHistory + "\r\n");
                    }
                }

            }
        }
    }
}
