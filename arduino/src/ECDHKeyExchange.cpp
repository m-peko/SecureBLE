/*
 * Copyright (C) 2018 Marin Peko
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

#include <ECDHKeyExchange.h>
#include <Curve25519.h>

ECDHKeyExchange::ECDHKeyExchange()
    : m_publicKey(),
      m_privateKey(),
      m_foreignPublicKey(),
      m_sharedSecret()
{}

ECDHKeyExchange::~ECDHKeyExchange()
{}

void
ECDHKeyExchange::setForeignPublicKey(char const *key)
{
    for (size_t i = 0; i < KEY_SIZE; i++)
    {
        sscanf(key, "%2hhx", &m_foreignPublicKey[i]);
        key += 2;
    }
}

char const *
ECDHKeyExchange::getPublicKeyStr()
{
    return keyToStr(m_publicKey);
}

char const *
ECDHKeyExchange::getPrivateKeyStr()
{
    return keyToStr(m_privateKey);
}

char const *
ECDHKeyExchange::getForeignPublicKeyStr()
{
    return keyToStr(m_foreignPublicKey);
}

void
ECDHKeyExchange::clearKeys()
{
    memset(m_publicKey, 0, KEY_SIZE);
    memset(m_privateKey, 0, KEY_SIZE);
    memset(m_foreignPublicKey, 0, KEY_SIZE);
    memset(m_sharedSecret, 0, KEY_SIZE);
}

void
ECDHKeyExchange::generateKeys()
{
    Curve25519::dh1(m_publicKey, m_privateKey);
}

bool
ECDHKeyExchange::generateSharedSecret()
{
    for (size_t i = 0; i < KEY_SIZE; i++)
    {
        m_sharedSecret[i] = m_foreignPublicKey[i];
    }

    if (!Curve25519::dh2(m_sharedSecret, m_privateKey))
    {
        return false;
    }
    return true;
}

char const *
ECDHKeyExchange::keyToStr(uint8_t const *key)
{
    uint8_t offset = 0;
    static char keyStr[2 * KEY_SIZE + 1];

    for (size_t i = 0; i < KEY_SIZE; i++)
    {
        offset += sprintf(keyStr + offset, "%02X", key[i]);
    }
    sprintf(keyStr + offset, "%c", '\0');

    return keyStr;
}
