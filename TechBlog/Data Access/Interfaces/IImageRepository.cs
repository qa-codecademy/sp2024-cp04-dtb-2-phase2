using Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        Image GetById(int? id);
    }
}
