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

#include <STS.h>
#include <utils/Utils.h>

namespace SecureBLE
{

STS::STS()
    : m_foreignSignature(),
      m_sessionKey()
{}

STS::~STS()
{}

void
STS::setSessionKey(char const *sharedSecret)
{
    // TODO(m-peko): Set session key from ECDH shared secret by hashing it with SHA256
    Utils::charArrayToByteArray(sharedSecret, m_sessionKey, KEY_SIZE);
}

void
STS::setForeignSignature(char const *signature)
{
    Utils::charArrayToByteArray(signature, m_foreignSignature, SIGNATURE_SIZE);
}

uint8_t const *
STS::createSignature(uint8_t const *keyA, uint8_t const *keyB)
{
    // TODO(m-peko): Create signature with Ed25519
    return nullptr;
}

bool
STS::verifyForeignSignature()
{
    // TODO(m-peko): Verify signature with Ed25519
    return true;
}

uint8_t const *
STS::encrypt(uint8_t const *payload)
{
    // TODO(m-peko): AES256 encryption with session key
    return nullptr;
}

uint8_t const *
STS::decrypt(uint8_t const *payload)
{
    // TODO(m-peko): AES256 decryption with session key
    return nullptr;
}

} /* SecureBLE */
