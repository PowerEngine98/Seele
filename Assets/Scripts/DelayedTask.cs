using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{
    public class DelayedTask
    {
        public static async Task Execute(float delay, VoidCallback callback)
        {
            await Task.Delay((int)(delay * 1000));
            callback();
        }

    }
}