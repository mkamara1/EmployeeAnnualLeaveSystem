using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSShippingLtd.Web.Interface
{
    public interface IEmployeeRespository <T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        IEnumerable<T> GetAllById(int Id);
        T Insert(T obj);
        void Delete(object obj);
        T Update(T obj);


    }
}