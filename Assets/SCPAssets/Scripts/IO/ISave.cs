using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Site13Kernel.IO
{
    public interface ISave
    {
        void Save();
        void Load();
    }
    public abstract class Savable : MonoBehaviour, ISave
    {
        public abstract void Load();

        public abstract void Save();
    }
}
