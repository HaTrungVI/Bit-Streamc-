using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NetworkTrungVI
{
    /// <summary>
    /// Ghi từng bit dữ liệu tuần tự
    /// </summary>
    public class OutputMemoryBitStream
    {
        private byte[] mBuffer;
        private int mBitHeader = 0;
        private int mBitCapacity = 0;
        public OutputMemoryBitStream()
        {
            mBuffer = null;
            mBitHeader = 0;
            mBitCapacity = 0;
            FixBuffer(1020 * 8);
        }
        public byte[] GetBuffer() { return mBuffer; }
        public int GetBitLength() { return mBitHeader; }
        public int GetByteLength() { return (mBitHeader + 7) >> 3; }
        private void FixBuffer(int _inLength)
        {
            // inBitCount dịch trái 3 bit tương ứng với inBitCount / 8.
            if (mBuffer == null)
            {
                mBuffer = new byte[_inLength >> 3];         //
                Array.Clear(mBuffer, 0, _inLength >> 3);    // dua du lieu ve 0
            }
            else
            {
                //Array.Resize(ref mBuffer, _inLength >> 3);
                byte[] mTempBuffer = new byte[_inLength >> 3];
                Array.Clear(mTempBuffer, 0, _inLength >> 3);  //
                Array.Copy(mBuffer, mTempBuffer, _inLength >> 3);

                mBuffer = mTempBuffer;
            }
            mBitCapacity = _inLength;
        }
        /// <summary>
        /// ghi 1 byte du lieu vao buffer 
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="inBitCount"></param>
        public void WriteBits(byte inData, int inBitCount)
        {
            // vị trí bit đầu tiên sau khi công thêm inBitCount.
            int mNextBitHeader = mBitHeader + inBitCount;
            if (mNextBitHeader > mBitCapacity)
            {
                // fix lại bố nhớ nếu số bit đã ghi hiện tại lớn hơn số bit có thể chứa.
                FixBuffer(Math.Max(mBitCapacity * 2, mNextBitHeader));
            }

            int ByteOffset = mBitHeader >> 3;   // trả về byte mà hiện tại chúng ta đang ghi dở
            int BitOffset = mBitHeader & 0x7;   // trả về số bit còn trống(chưa ghi) tại byte chúng ta đang ghi dở.

            // ghi dữ liệu của inData và các bit trống của byte hiện tại đang ghi dở.
            byte BitCover = (byte)(~(0xff << BitOffset));
            mBuffer[ByteOffset] = (byte)((mBuffer[ByteOffset] & BitCover) | (inData << BitOffset));

            int BitWrite = 8 - BitOffset;                              // số bit đã được ghi. Chúng ta phải ghi nốt các bit còn lại vào byte tiếp theo
            if (BitOffset < inBitCount)                                 // nếu số bit đã ghi = số InBitCount thì tức là đã ghi hết.
            {
                mBuffer[ByteOffset + 1] = (byte)(inData >> BitWrite);  // ghi nốt các bit còn lại vào trong byte tiếp theo.
            }
            mBitHeader = mNextBitHeader;
        }
        public void Write(int inData, int inBitCount = sizeof(int) * 8)
        {
            byte[] ConvertData = BitConverter.GetBytes(inData);
            int dataIndex = 0;
            while (inBitCount > 8)
            {
                WriteBits(ConvertData[dataIndex], 8);
                inBitCount-=8;
                ++dataIndex;
            }
            if (inBitCount > 0)
            {
                WriteBits(ConvertData[dataIndex], inBitCount);
            }
        }
        /// <summary>
        /// Ghi du lieu la kieu float
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="inBitCount"></param>
        public void Write(float inData, int inBitCount = sizeof(float) * 8)
        {
            byte[] ConvertData = BitConverter.GetBytes(inData);
            int dataIndex = 0;
            while (inBitCount > 8)
            {
                WriteBits(ConvertData[dataIndex], 8);
                inBitCount -= 8;
                ++dataIndex;
            }
            if (inBitCount > 0)
            {
                WriteBits(ConvertData[dataIndex], inBitCount);
            }
        }
        public void Write(double inData, int inBitCount = sizeof(double) * 8)
        {
            byte[] ConvertData = BitConverter.GetBytes(inData);
            int dataIndex = 0;
            while (inBitCount > 8)
            {
                WriteBits(ConvertData[dataIndex], 8);
                inBitCount -= 8;
                ++dataIndex;
            }
            if (inBitCount > 0)
            {
                WriteBits(ConvertData[dataIndex], inBitCount);
            }
        }
        public void Write(bool inData, int inBitCount = sizeof(bool) * 8)
        {
            byte[] ConvertData = BitConverter.GetBytes(inData);
            int dataIndex = 0;
            while (inBitCount > 8)
            {
                WriteBits(ConvertData[dataIndex], 8);
                inBitCount -= 8;
                ++dataIndex;
            }
            if (inBitCount > 0)
            {
                WriteBits(ConvertData[dataIndex], inBitCount);
            }
        }
        public void Write(string inData)
        {
            // ĐỂ ghi 1 chuỗi đầu tiên chúng ta cần ghi số bit độ dài của chuỗi.
            int inBitCount = inData.Length * 8;
            Write(inBitCount);
            byte[] ConvertData = Encoding.ASCII.GetBytes(inData);
            int dataIndex = 0;
            while (inBitCount > 8)
            {
                WriteBits(ConvertData[dataIndex], 8);
                inBitCount -= 8;
                ++dataIndex;
            }
            if (inBitCount > 0)
            {
                WriteBits(ConvertData[dataIndex], inBitCount);
            }
        }

        public void Write(UInt32 inData, int inBitCount = sizeof(UInt32) * 8)
        {
            byte[] ConvertData = BitConverter.GetBytes(inData);
            int dataIndex = 0;
            while (inBitCount > 8)
            {
                WriteBits(ConvertData[dataIndex], 8);
                inBitCount -= 8;
                ++dataIndex;
            }
            if (inBitCount > 0)
            {
                WriteBits(ConvertData[dataIndex], inBitCount);
            }
        }

        public void Write(UInt16 inData, int inBitCount = sizeof(UInt16) * 8)
        {
            byte[] ConvertData = BitConverter.GetBytes(inData);
            int dataIndex = 0;
            while (inBitCount > 8)
            {
                WriteBits(ConvertData[dataIndex], 8);
                inBitCount -= 8;
                ++dataIndex;
            }
            if (inBitCount > 0)
            {
                WriteBits(ConvertData[dataIndex], inBitCount);
            }
        }

        public void Write(UInt64 inData, int inBitCount = sizeof(UInt64) * 8)
        {
            byte[] ConvertData = BitConverter.GetBytes(inData);
            int dataIndex = 0;
            while (inBitCount > 8)
            {
                WriteBits(ConvertData[dataIndex], 8);
                inBitCount -= 8;
                ++dataIndex;
            }
            if (inBitCount > 0)
            {
                WriteBits(ConvertData[dataIndex], inBitCount);
            }
        }

        #region nếu bạn không dùng unity engine thì có thể xóa phần này
        public void Write(Vector3 inData)
        {
            Write(inData.x);
            Write(inData.y);
            Write(inData.z);
        }
        public void Write(Quaternion inData)
        {
            Write(inData.x);
            Write(inData.y);
            Write(inData.z);
            Write(inData.w);
        }
        public void Write(Vector2 inData)
        {
            Write(inData.x);
            Write(inData.y);
        }
        #endregion
    }
}
