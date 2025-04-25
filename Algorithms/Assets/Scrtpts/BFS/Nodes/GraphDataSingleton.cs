using UnityEngine;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Assets.Scrtpts.BFS.Nodes
{
    public class GraphDataSingleton : MonoBehaviour
    {
        public static GraphDataSingleton Instance;
        public GraphData graphData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
