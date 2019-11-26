using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.seele
{
    public class CollectableTriggerItem : CollectableItem
    {
        public Executable executable;

        public void OnPlayerCollision()
        {
            if (executable != null)
            {
                executable.Execute();
            }
            base.OnPlayerCollision();
        }
    }
}

