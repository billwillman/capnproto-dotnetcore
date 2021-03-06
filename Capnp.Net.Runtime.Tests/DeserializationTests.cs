﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capnp.Net.Runtime.Tests
{
    [TestClass]
    public class DeserializationTests
    {
        [TestMethod]
        public void ListOfBytesAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(8, 10);
            ds.ListWriteValue(1, (byte)0x11);
            ds.ListWriteValue(2, (byte)0x22);
            ds.ListWriteValue(3, (byte)0x33);
            ds.ListWriteValue(4, (byte)0x44);
            ds.ListWriteValue(5, (byte)0x55);
            ds.ListWriteValue(6, (byte)0x66);
            ds.ListWriteValue(7, (byte)0x77);
            ds.ListWriteValue(8, (byte)0x88);
            ds.ListWriteValue(9, (byte)0x99);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(10, asListOfStructs.Count);
            Assert.AreEqual(ObjectKind.Value, asListOfStructs[0].Kind);
            Assert.AreEqual((byte)0x00, asListOfStructs[0].ReadDataByte(0));
            Assert.AreEqual((byte)0x11, asListOfStructs[1].ReadDataByte(0));
            Assert.AreEqual((byte)0x22, asListOfStructs[2].ReadDataByte(0));
            Assert.AreEqual((byte)0x33, asListOfStructs[3].ReadDataByte(0));
            Assert.AreEqual((byte)0x44, asListOfStructs[4].ReadDataByte(0));
            Assert.AreEqual((byte)0x55, asListOfStructs[5].ReadDataByte(0));
            Assert.AreEqual((byte)0x66, asListOfStructs[6].ReadDataByte(0));
            Assert.AreEqual((byte)0x77, asListOfStructs[7].ReadDataByte(0));
            Assert.AreEqual((byte)0x88, asListOfStructs[8].ReadDataByte(0));
            Assert.AreEqual((byte)0x99, asListOfStructs[9].ReadDataByte(0));
        }

        [TestMethod]
        public void ListOfSBytesAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(8, 10);
            ds.ListWriteValue(1, (sbyte)-1);
            ds.ListWriteValue(2, (sbyte)-2);
            ds.ListWriteValue(3, (sbyte)-3);
            ds.ListWriteValue(4, (sbyte)-4);
            ds.ListWriteValue(5, (sbyte)-5);
            ds.ListWriteValue(6, (sbyte)-6);
            ds.ListWriteValue(7, (sbyte)-7);
            ds.ListWriteValue(8, (sbyte)-8);
            ds.ListWriteValue(9, (sbyte)-9);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(10, asListOfStructs.Count);
            Assert.AreEqual(ObjectKind.Value, asListOfStructs[0].Kind);
            Assert.AreEqual((sbyte)0, asListOfStructs[0].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-1, asListOfStructs[1].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-2, asListOfStructs[2].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-3, asListOfStructs[3].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-4, asListOfStructs[4].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-5, asListOfStructs[5].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-6, asListOfStructs[6].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-7, asListOfStructs[7].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-8, asListOfStructs[8].ReadDataSByte(0));
            Assert.AreEqual((sbyte)-9, asListOfStructs[9].ReadDataSByte(0));
        }

        [TestMethod]
        public void ListOfUShortsAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(16, 3);
            ds.ListWriteValue(1, (ushort)0x1111);
            ds.ListWriteValue(2, (ushort)0x2222);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(3, asListOfStructs.Count);
            Assert.AreEqual(ObjectKind.Value, asListOfStructs[0].Kind);
            Assert.AreEqual((ushort)0x0000, asListOfStructs[0].ReadDataUShort(0));
            Assert.AreEqual((ushort)0x1111, asListOfStructs[1].ReadDataUShort(0));
            Assert.AreEqual((ushort)0x2222, asListOfStructs[2].ReadDataUShort(0));
        }

        [TestMethod]
        public void ListOfShortsAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(16, 3);
            ds.ListWriteValue(1, (short)-0x1111);
            ds.ListWriteValue(2, (short)-0x2222);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(3, asListOfStructs.Count);
            Assert.AreEqual(ObjectKind.Value, asListOfStructs[0].Kind);
            Assert.AreEqual((short)0, asListOfStructs[0].ReadDataShort(0));
            Assert.AreEqual((short)-0x1111, asListOfStructs[1].ReadDataShort(0));
            Assert.AreEqual((short)-0x2222, asListOfStructs[2].ReadDataShort(0));
        }

        [TestMethod]
        public void ListOfUIntsAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(32, 2);
            ds.ListWriteValue(1, uint.MaxValue);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(2, asListOfStructs.Count);
            Assert.AreEqual(ObjectKind.Value, asListOfStructs[0].Kind);
            Assert.AreEqual(0u, asListOfStructs[0].ReadDataUInt(0));
            Assert.AreEqual(uint.MaxValue, asListOfStructs[1].ReadDataUInt(0));
        }

        [TestMethod]
        public void ListOfIntsAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(32, 2);
            ds.ListWriteValue(0, int.MinValue);
            ds.ListWriteValue(1, int.MaxValue);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(2, asListOfStructs.Count);
            Assert.AreEqual(ObjectKind.Value, asListOfStructs[0].Kind);
            Assert.AreEqual(int.MinValue, asListOfStructs[0].ReadDataInt(0));
            Assert.AreEqual(int.MaxValue, asListOfStructs[1].ReadDataInt(0));
        }

        [TestMethod]
        public void ListOfULongsAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(64, 2);
            ds.ListWriteValue(1, ulong.MaxValue);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(2, asListOfStructs.Count);
            Assert.AreEqual(0ul, asListOfStructs[0].ReadDataULong(0));
            Assert.AreEqual(ulong.MaxValue, asListOfStructs[1].ReadDataULong(0));
        }

        [TestMethod]
        public void ListOfLongsAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(64, 2);
            ds.ListWriteValue(0, long.MinValue);
            ds.ListWriteValue(1, long.MaxValue);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(2, asListOfStructs.Count);
            Assert.AreEqual(long.MinValue, asListOfStructs[0].ReadDataLong(0));
            Assert.AreEqual(long.MaxValue, asListOfStructs[1].ReadDataLong(0));
        }

        [TestMethod]
        public void ListOfFloatsAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(32, 6);
            ds.ListWriteValue(0, 1.0f);
            ds.ListWriteValue(1, float.MinValue);
            ds.ListWriteValue(2, float.MaxValue);
            ds.ListWriteValue(3, float.NaN);
            ds.ListWriteValue(4, float.NegativeInfinity);
            ds.ListWriteValue(5, float.PositiveInfinity);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(6, asListOfStructs.Count);
            Assert.AreEqual(1.0f, asListOfStructs[0].ReadDataFloat(0));
            Assert.AreEqual(float.MinValue, asListOfStructs[1].ReadDataFloat(0));
            Assert.AreEqual(float.MaxValue, asListOfStructs[2].ReadDataFloat(0));
            Assert.AreEqual(float.NaN, asListOfStructs[3].ReadDataFloat(0));
            Assert.AreEqual(float.NegativeInfinity, asListOfStructs[4].ReadDataFloat(0));
            Assert.AreEqual(float.PositiveInfinity, asListOfStructs[5].ReadDataFloat(0));
        }

        [TestMethod]
        public void ListOfDoublesAsListOfStructs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfValues(64, 6);
            ds.ListWriteValue(0, 1.0);
            ds.ListWriteValue(1, double.MinValue);
            ds.ListWriteValue(2, double.MaxValue);
            ds.ListWriteValue(3, double.NaN);
            ds.ListWriteValue(4, double.NegativeInfinity);
            ds.ListWriteValue(5, double.PositiveInfinity);

            DeserializerState d = ds;
            var asListOfStructs = d.RequireList().Cast(_ => _);
            Assert.AreEqual(6, asListOfStructs.Count);
            Assert.AreEqual(1.0, asListOfStructs[0].ReadDataDouble(0));
            Assert.AreEqual(double.MinValue, asListOfStructs[1].ReadDataDouble(0));
            Assert.AreEqual(double.MaxValue, asListOfStructs[2].ReadDataDouble(0));
            Assert.AreEqual(double.NaN, asListOfStructs[3].ReadDataDouble(0));
            Assert.AreEqual(double.NegativeInfinity, asListOfStructs[4].ReadDataDouble(0));
            Assert.AreEqual(double.PositiveInfinity, asListOfStructs[5].ReadDataDouble(0));
        }

        [TestMethod]
        public void ListOfStructsAsListOfBytes()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(3, 1, 0);
            ds.ListBuildStruct(1).WriteData(0, (byte)0x11);
            ds.ListBuildStruct(2).WriteData(0, (byte)0x22);

            DeserializerState d = ds;
            var asListOfBytes = d.RequireList().CastByte();
            Assert.AreEqual(3, asListOfBytes.Count);
            Assert.AreEqual(0, asListOfBytes[0]);
            Assert.AreEqual((byte)0x11, asListOfBytes[1]);
            Assert.AreEqual((byte)0x22, asListOfBytes[2]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfSBytes()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(1).WriteData(0, sbyte.MinValue);

            DeserializerState d = ds;
            var asListOfSBytes = d.RequireList().CastSByte();
            Assert.AreEqual(2, asListOfSBytes.Count);
            Assert.AreEqual((sbyte)0, asListOfSBytes[0]);
            Assert.AreEqual(sbyte.MinValue, asListOfSBytes[1]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfUShorts()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(1).WriteData(0, ushort.MaxValue);

            DeserializerState d = ds;
            var asListOfUShorts = d.RequireList().CastUShort();
            Assert.AreEqual(2, asListOfUShorts.Count);
            Assert.AreEqual((ushort)0, asListOfUShorts[0]);
            Assert.AreEqual(ushort.MaxValue, asListOfUShorts[1]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfShorts()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(0).WriteData(0, short.MinValue);
            ds.ListBuildStruct(1).WriteData(0, short.MaxValue);

            DeserializerState d = ds;
            var asListOfShorts = d.RequireList().CastShort();
            Assert.AreEqual(2, asListOfShorts.Count);
            Assert.AreEqual(short.MinValue, asListOfShorts[0]);
            Assert.AreEqual(short.MaxValue, asListOfShorts[1]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfUInts()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(1).WriteData(0, uint.MaxValue);

            DeserializerState d = ds;
            var asListOfUInts = d.RequireList().CastUInt();
            Assert.AreEqual(2, asListOfUInts.Count);
            Assert.AreEqual(0u, asListOfUInts[0]);
            Assert.AreEqual(uint.MaxValue, asListOfUInts[1]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfInts()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(0).WriteData(0, int.MinValue);
            ds.ListBuildStruct(1).WriteData(0, int.MaxValue);

            DeserializerState d = ds;
            var asListOfInts = d.RequireList().CastInt();
            Assert.AreEqual(2, asListOfInts.Count);
            Assert.AreEqual(int.MinValue, asListOfInts[0]);
            Assert.AreEqual(int.MaxValue, asListOfInts[1]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfULongs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(1).WriteData(0, ulong.MaxValue);

            DeserializerState d = ds;
            var asListOfULongs = d.RequireList().CastULong();
            Assert.AreEqual(2, asListOfULongs.Count);
            Assert.AreEqual(0ul, asListOfULongs[0]);
            Assert.AreEqual(ulong.MaxValue, asListOfULongs[1]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfLongs()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(0).WriteData(0, long.MinValue);
            ds.ListBuildStruct(1).WriteData(0, long.MaxValue);

            DeserializerState d = ds;
            var asListOfLongs = d.RequireList().CastLong();
            Assert.AreEqual(2, asListOfLongs.Count);
            Assert.AreEqual(long.MinValue, asListOfLongs[0]);
            Assert.AreEqual(long.MaxValue, asListOfLongs[1]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfFloats()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(0).WriteData(0, float.NaN);
            ds.ListBuildStruct(1).WriteData(0, float.PositiveInfinity);

            DeserializerState d = ds;
            var asListOfFloats = d.RequireList().CastFloat();
            Assert.AreEqual(2, asListOfFloats.Count);
            Assert.AreEqual(float.NaN, asListOfFloats[0]);
            Assert.AreEqual(float.PositiveInfinity, asListOfFloats[1]);
        }

        [TestMethod]
        public void ListOfStructsAsListOfDoubles()
        {
            var ds = new DynamicSerializerState(MessageBuilder.Create(128));
            ds.SetListOfStructs(2, 1, 0);
            ds.ListBuildStruct(0).WriteData(0, double.NegativeInfinity);
            ds.ListBuildStruct(1).WriteData(0, double.MaxValue);

            DeserializerState d = ds;
            var asListOfDoubles = d.RequireList().CastDouble();
            Assert.AreEqual(2, asListOfDoubles.Count);
            Assert.AreEqual(double.NegativeInfinity, asListOfDoubles[0]);
            Assert.AreEqual(double.MaxValue, asListOfDoubles[1]);
        }
    }
}
