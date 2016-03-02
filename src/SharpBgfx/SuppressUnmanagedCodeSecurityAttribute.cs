using System;

#if PORTABLE
namespace SharpBgfx
{
    internal class SuppressUnmanagedCodeSecurityAttribute : Attribute
    {
    }
}
#endif