using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace NetworkTrungVI
{
    public class OutputMemoryStream
    {
        private MemoryStream mMemoryStream;
        private int mHeader = 0;
        private byte[] mDataMemory, mBuffer;

        private void FixCapacity(int _Capacity)
        {
            mMemoryStream.Capacity = _Capacity;
        }
        public OutputMemoryStream()
        {
            mMemoryStream = new MemoryStream();
            mHeader = 0;
        }

        public byte[] GetData()
        {
            mDataMemory = mMemoryStream.GetBuffer();
            return mDataMemory;
        }

        #region Write method
        /// <summary>
        /// Viet tuan tu 1 int
        /// </summary>
        /// <param name="inData"></param>
        public void Write(int inData)
        {
            mBuffer = BitConverter.GetBytes(inData);
            mMemoryStream.Write(mBuffer, 0, mBuffer.Length);
        }
        /// <summary>
        /// Viet tuan tu 1 float
        /// </summary>
        /// <param name="inData"></param>
        public void Write(float inData)
        {
            mBuffer = BitConverter.GetBytes(inData);
            mMemoryStream.Write(mBuffer, 0, mBuffer.Length);
        }
        /// <summary>
        /// Viet tuan tu 1 boolean
        /// </summary>
        /// <param name="inData"></param>
        public void Write(bool inData)
        {
            mBuffer = BitConverter.GetBytes(inData);
            mMemoryStream.Write(mBuffer, 0, mBuffer.Length);
        }
        /// <summary>
        /// Viet tuan tu 1 so kieu int 16 bit
        /// </summary>
        /// <param name="inData"></param>
        public void Write(Int16 inData)
        {
            mBuffer = BitConverter.GetBytes(inData);
            mMemoryStream.Write(mBuffer, 0, mBuffer.Length);
        }
        /// <summary>
        /// Viet tuan tu 1 so kieu double
        /// </summary>
        /// <param name="inData"></param>
        public void Write(double inData)
        {
            mBuffer = BitConverter.GetBytes(inData);
            mMemoryStream.Write(mBuffer, 0, mBuffer.Length);
        }
        /// <summary>
        /// Viet tuan tu 1 so kieu uint32
        /// </summary>
        /// <param name="inData"></param>
        public void Write(UInt32 inData)
        {
            mBuffer = BitConverter.GetBytes(inData);
            mMemoryStream.Write(mBuffer, 0, mBuffer.Length);
        }
        /// <summary>
        /// Ghi 1 chuoi. bang cach ghi do dai, sau do ghi chuoi ky tu do xuong.
        /// </summary>
        /// <param name="inData"></param>
        public void Write(string inData)
        {
            mBuffer = Encoding.ASCII.GetBytes(inData);
            Write(mBuffer.Length);
            mBuffer = Encoding.ASCII.GetBytes(inData);
            mMemoryStream.Write(mBuffer, 0, mBuffer.Length);
        }

        /// <summary>
        /// doc 1 vector3 tu 1 stream
        /// </summary>
        /// <param name="inData"></param>
        public void Write(Vector3 inData)
        {
            Write(inData.x);
            Write(inData.y);
            Write(inData.z);
        }
        /// <summary>
        /// doc 1 quaternion tu 1 stream
        /// </summary>
        /// <param name="inData"></param>
        public void Write(Quaternion inData)
        {
            Write(inData.x);
            Write(inData.y);
            Write(inData.z);
            Write(inData.w);
        }
        /// <summary>
        /// doc 1 vector2 tu 1 stream
        /// </summary>
        /// <param name="inData"></param>
        public void Write(Vector2 inData)
        {
            Write(inData.x);
            Write(inData.y);
        }

        /// <summary>
        /// Su dung cho lop tu dinh nghia
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inData"></param>
        public virtual void Write<T>(T inData) { return; }
        #endregion
    }
}
