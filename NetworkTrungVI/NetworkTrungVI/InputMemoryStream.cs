using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace NetworkTrungVI
{
    public class InputMemoryStream
    {
        private byte[] mDataMemory;
        private int mOffset = 0;
        private int mBufferLength = 0;
        public InputMemoryStream(byte[] _inData)
        {
            mDataMemory = _inData;
            mOffset = 0;
            mBufferLength = _inData.Length;
        }
        public InputMemoryStream(byte[] _inData, int _inByteCount)
        {
            mDataMemory = _inData;
            mOffset = 0;
            mBufferLength = _inByteCount;
        }
        public int GetRemainingByteCount() { return mBufferLength - mOffset; }

        #region Read method
        public void Read(ref int outData)
        {
            outData = BitConverter.ToInt32(mDataMemory, mOffset);
            mOffset += sizeof(int);
        }
        public void Read(ref float outData)
        {
            outData = BitConverter.ToSingle(mDataMemory, mOffset);
            mOffset += sizeof(float);
        }
        public void Read(ref bool outData)
        {
            outData = BitConverter.ToBoolean(mDataMemory, mOffset);
            mOffset += sizeof(bool);
        }
        public void Read(ref Int16 outData)
        {
            outData = BitConverter.ToInt16(mDataMemory, mOffset);
            mOffset += sizeof(Int16);
        }
        public void Read(ref double outData)
        {
            outData = BitConverter.ToDouble(mDataMemory, mOffset);
            mOffset += sizeof(double);
        }
        public void Read(ref UInt32 outData)
        {
            outData = BitConverter.ToUInt32(mDataMemory, mOffset);
            mOffset += sizeof(UInt32);
        }

        public void Read(ref string outData)
        {
            int length = 0;
            Read(ref length);
            outData = Encoding.ASCII.GetString(mDataMemory, mOffset, length);
            mOffset += length;
        }

        /// <summary>
        /// Ghi tuan tu 1 vector3 xuong 1 stream
        /// </summary>
        /// <param name="outData"></param>
        public void Read(ref Vector3 outData)
        {
            Read(ref outData.x);
            Read(ref outData.y);
            Read(ref outData.z);
        }
        /// <summary>
        /// ghi tuan tu 1 quaternion xuong 1 stream
        /// </summary>
        /// <param name="outData"></param>
        public void Read(ref Quaternion outData)
        {
            Read(ref outData.x);
            Read(ref outData.y);
            Read(ref outData.z);
            Read(ref outData.w);
        }
        /// <summary>
        /// ghi tuan tu 1 vector2 xuong 1 stream
        /// </summary>
        /// <param name="outData"></param>
        public void Read(ref Vector2 outData)
        {
            Read(ref outData.x);
            Read(ref outData.y);
        }

        public virtual void Read<T>(ref T outData) { return; }
        #endregion
    }
}
