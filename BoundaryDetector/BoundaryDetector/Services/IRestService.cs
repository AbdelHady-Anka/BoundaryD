using System;
using System.Collections.Generic;
using System.Text;

namespace BoundaryDetector.Services
{
    public interface IRestService<T>
        where T : new()
    {
        T Post(double longitude, double latitude);
    }
}
