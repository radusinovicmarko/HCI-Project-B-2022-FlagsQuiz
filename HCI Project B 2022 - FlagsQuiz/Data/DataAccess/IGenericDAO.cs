using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project_B_2022___FlagsQuiz.Data.DataAccess
{
    internal interface IGenericDAO<T>
    {
        List<T> GetAll();
        List<T> Get(T item);
        int Add(T item);
    }
}
