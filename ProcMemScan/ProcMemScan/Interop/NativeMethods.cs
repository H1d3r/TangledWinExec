﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcMemScan.Interop
{
    using NTSTATUS = Int32;
    using SIZE_T = UIntPtr;

    internal class NativeMethods
    {
        /*
         * kernel32.dll
         */
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FileTimeToSystemTime(
            in LARGE_INTEGER lpFileTime,
            out SYSTEMTIME lpSystemTime);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int FormatMessage(
            FormatMessageFlags dwFlags,
            IntPtr lpSource,
            int dwMessageId,
            int dwLanguageId,
            StringBuilder lpBuffer,
            int nSize,
            IntPtr Arguments);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SystemTimeToTzSpecificLocalTime(
            IntPtr lpTimeZoneInformation,
            in SYSTEMTIME lpUniversalTime,
            out SYSTEMTIME lpLocalTime);

        /*
         * ntdll.dll
         */
        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtClose(IntPtr Handle);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtCreateFile(
            out IntPtr FileHandle,
            ACCESS_MASK DesiredAccess,
            in OBJECT_ATTRIBUTES ObjectAttributes,
            out IO_STATUS_BLOCK IoStatusBlock,
            IntPtr /* PLARGE_INTEGER */ AllocationSize,
            FILE_ATTRIBUTE_FLAGS FileAttributes,
            FILE_SHARE_ACCESS ShareAccess,
            FILE_CREATE_DISPOSITION CreateDisposition,
            FILE_CREATE_OPTIONS CreateOptions,
            IntPtr EaBuffer,
            uint EaLength);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtOpenProcess(
            out IntPtr ProcessHandle,
            ACCESS_MASK DesiredAccess,
            in OBJECT_ATTRIBUTES ObjectAttributes,
            in CLIENT_ID ClientId);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtOpenSymbolicLinkObject(
            out IntPtr LinkHandle,
            ACCESS_MASK DesiredAccess,
            in OBJECT_ATTRIBUTES ObjectAttributes);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtQueryInformationProcess(
            IntPtr ProcessHandle,
            PROCESSINFOCLASS ProcessInformationClass,
            IntPtr pProcessInformation,
            uint ProcessInformationLength,
            out uint ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtQueryInformationProcess(
            IntPtr ProcessHandle,
            PROCESSINFOCLASS ProcessInformationClass,
            IntPtr pProcessInformation,
            uint ProcessInformationLength,
            IntPtr ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtQueryObject(
            IntPtr Handle,
            OBJECT_INFORMATION_CLASS ObjectInformationClass,
            IntPtr pObjectInformation,
            uint ObjectInformationLength,
            out uint ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtQuerySymbolicLinkObject(
            IntPtr LinkHandle,
            IntPtr /* PUNICODE_STRING */ LinkTarget,
            out uint ReturnedLength);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtQueryVirtualMemory(
            IntPtr ProcessHandle,
            IntPtr BaseAddress,
            MEMORY_INFORMATION_CLASS MemoryInformationClass,
            IntPtr MemoryInformation,
            SIZE_T MemoryInformationLength,
            out SIZE_T ReturnLength);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtReadVirtualMemory(
            IntPtr ProcessHandle,
            IntPtr BaseAddress,
            IntPtr Buffer,
            uint NumberOfBytesToRead,
            out uint NumberOfBytesReaded);

        [DllImport("ntdll.dll")]
        public static extern NTSTATUS NtWriteFile(
            IntPtr FileHandle,
            IntPtr Event,
            IntPtr ApcRoutine, // Reserved parameter. Should be null.
            IntPtr ApcContext, // Reserved parameter. Should be null.
            out IO_STATUS_BLOCK IoStatusBlock,
            IntPtr Buffer,
            uint Length,
            IntPtr /* in LARGE_INTEGER */ ByteOffset,
            IntPtr Key); // Should be null.

        [DllImport("ntdll.dll")]
        public static extern uint RtlNtStatusToDosError(NTSTATUS Status);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern void RtlSetLastWin32Error(int dwErrCode);
    }
}
