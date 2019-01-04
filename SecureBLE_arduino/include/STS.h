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

#ifndef STS_H
#define STS_H

#include <stdint.h>
#include <stddef.h>

namespace SecureBLE
{

class STS
{
public:
    STS() noexcept;
    ~STS() noexcept;

    uint8_t const * createSignature(uint8_t const *keyA, uint8_t const *keyB);
    bool verifySignature(uint8_t const* payload);

    uint8_t const * encrypt(uint8_t const *payload);
    uint8_t const * decrypt(uint8_t const *payload);

private:
    static constexpr size_t KEY_SIZE = 32;

    uint8_t m_sessionKey[KEY_SIZE];
};

} /* SecureBLE */

#endif /* STS_H */
