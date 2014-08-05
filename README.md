## SharpBgfx

Provides managed (C#,VB,F#,etc) bindings for the bgfx graphics library.

See <https://github.com/bkaradzic/bgfx>.

The main library is a minimal set of pinvoke declarations. The examples pull in
additional dependencies for various bits of helper functionality, such as the
[SlimMath](https://code.google.com/p/slimmath/) vector math library.

### Platforms

Currently only tested on Windows, though it will probably run fine on Mac and Linux if Mono is installed and the bgfx native library is rebuilt for those platforms.
