using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkTrungVI
{
    public class InputMemoryBitStream
    {
        private byte[] mBuffer;
        private int mBitHeader = 0;
        private int mBitCapacity = 0;

        public InputMemoryBitStream(byte[] inData)
        {
            mBuffer = inData;
            mBitHeader = 0;
            mBitCapacity = inData.Length * 8;
        }
        public InputMemoryBitStream(byte[] inData, int inBitLength)
        {
            mBuffer = inData;
            mBitHeader = 0;
            mBitCapacity = inBitLength;
        }
        public int GetRemainingBitsCount() { return mBitCapacity - mBitHeader; }
        public void ReadBits(ref byte outData, int inBitCount)
        {
            // quá trình đọc dữ liệu ra chỉ ngược lại so với quá trình ghi
            // sẽ dễ dàng hiểu quá trình đọc nếu hiểu quá trình ghi.
            int byteOffset = mBitHeader >> 3;
            int bitOffset = mBitHeader & 0x7;

            outData = (byte)(mBuffer[byteOffset] >> bitOffset);

            int BitRead = 8 - bitOffset;
            if (BitRead < inBitCount)
            {
                outData |= (byte)(mBuffer[byteOffset + 1] << BitRead);
            }
            mBitHeader += inBitCount;
        }
        public void Read(ref int outData, int inLength = sizeof(int) * 8)
        {
            byte[] ConvertData = new byte[inLength >> 3];
            int dataIndex = 0;
            while (inLength > 8)
            {
                ReadBits(ref ConvertData[dataIndex], 8);
                inLength -= 8;
                ++dataIndex;
            }
            if (inLength > 0)
            {
                ReadBits(ref ConvertData[dataIndex], inLength);
            }
            outData = BitConverter.ToInt32(ConvertData, 0);
        }
        public void Read(ref float outData, int inLength = sizeof(float) * 8)
        {
            byte[] ConvertData = new byte[inLength >> 3];
            int dataIndex = 0;
            while (inLength > 8)
            {
                ReadBits(ref ConvertData[dataIndex], 8);
                inLength -= 8;
                ++dataIndex;
            }
            if (inLength > 0)
            {
                ReadBits(ref ConvertData[dataIndex], inLength);
            }
            outData = BitConverter.ToSingle(ConvertData, 0);
        }
        public void Read(ref double outData, int inLength = sizeof(double) * 8)
        {
            byte[] ConvertData = new byte[inLength >> 3];
            int dataIndex = 0;
            while (inLength > 8)
            {
                ReadBits(ref ConvertData[dataIndex], 8);
                inLength -= 8;
                ++dataIndex;
            }
            if (inLength > 0)
            {
                ReadBits(ref ConvertData[dataIndex], inLength);
            }
            outData = BitConverter.ToDouble(ConvertData, 0);
        }
        public void Read(ref bool outData, int inLength = sizeof(bool) * 8)
        {
            byte[] ConvertData = new byte[inLength >> 3];
            int dataIndex = 0;
            while (inLength > 8)
            {
                ReadBits(ref ConvertData[dataIndex], 8);
                inLength -= 8;
                ++dataIndex;
            }
            if (inLength > 0)
            {
                ReadBits(ref ConvertData[dataIndex], inLength);
            }
            outData = BitConverter.ToBoolean(ConvertData, 0);
        }
        public void Read(ref string outData)
        {
            int inLength = 0;
            Read(ref inLength);
            byte[] ConvertData = new byte[inLength >> 3];
            int dataIndex = 0;
            while (inLength > 8)
            {
                ReadBits(ref ConvertData[dataIndex], 8);
                inLength -= 8;
                ++dataIndex;
            }
            if (inLength > 0)
            {
                ReadBits(ref ConvertData[dataIndex], inLength);
            }
            outData = Encoding.ASCII.GetString(ConvertData);
        }

        #region nếu bạn không dùng unity engine thì có thể xóa phần này
        public void Read(ref Vector3 outData)
        {
            Read(ref outData.x);
            Read(ref outData.y);
            Read(ref outData.z);
        }
        public void Read(ref Quaternion outData)
        {
            Read(ref outData.x);
            Read(ref outData.y);
            Read(ref outData.z);
            Read(ref outData.w);
        }
        public void Read(ref Vector2 outData)
        {
            Read(ref outData.x);
            Read(ref outData.y);
        }
        #endregion
    }
}
