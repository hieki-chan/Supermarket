﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UR = UnityEngine.Random;

namespace Supermarket.Customers
{
    public class MovementPath : MonoBehaviour
    {
        public List<PathNode> Nodes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Paths;
        }

        [SerializeField]
        private List<PathNode> Paths;

        public Vector3 this[int index, bool global = false]
        {
            get
            {
                return global ? Paths[index].vertex + transform.position : Paths[index].vertex;
            }

            set
            {
                PathNode node = Paths[index];
                node.vertex = global ? value : value - transform.position;
                Paths[index] = node;
            }
        }

        public int Count => Paths.Count;

#if UNITY_EDITOR
        private void OnValidate()
        {
            //for (int i = 0; i < Count; i++)
            //{
            //    Node v = Nodes[i];
            //    v.connectTo = Mathf.Clamp(v.connectTo, 0, Paths.Count - 1);
            //    Nodes[i] = v;
            //}
        }
#endif
    }
    [Serializable]
    public struct PathNode
    {
        public Vector3 vertex;
        public float rotateY;
        public float range;

        //public bool connected;
        //public int connectTo;

        public Vector3 PositionInRange() => vertex + new Vector3(UR.Range(0, 1f) * range, 0, UR.Range(0, 1f) * range);
    }
}