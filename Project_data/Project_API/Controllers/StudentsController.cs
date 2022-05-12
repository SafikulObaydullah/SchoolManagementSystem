using Project_data;
using Project_data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Project_API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class StudentsController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public StudentsController()
        {

        }
        public string Post(Student student)
        {
            repositoryAccess.StudentRepo.Add(student);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.StudentRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Student> Get()
        {
            return repositoryAccess.StudentRepo.GetAll();
        }
        public Student Get(int id)
        {
            return repositoryAccess.StudentRepo.GetById(id);
        }
        public string Put(Student student)
        {
            repositoryAccess.StudentRepo.update(student);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
