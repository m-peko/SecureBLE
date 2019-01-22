/*
 * Copyright (C) 2019 Marin Peko
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

#include <utils/Utils.h>

namespace SecureBLE
{

namespace Utils
{

void
charArrayToByteArray(char const *src, uint8_t *dst, size_t const size)
{
    for (size_t i = 0; i < size; i++)
    {
        sscanf(src, "%2hhx", dst + i);
        src += 2;
    }
}

void
byteArrayToCharArray(uint8_t const *src, char *dst, size_t const size)
{
    uint8_t offset = 0;

    for (size_t i = 0; i < size; i++)
    {
        offset += sprintf(dst + offset, "%02X", src[i]);
    }
    sprintf(dst + offset, "%c", '\0');
}

} /* Utils */

} /* SecureBLE */