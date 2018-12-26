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

#ifndef MessageParser_H
#define MessageParser_H

#include <WString.h>

namespace SecureBLE
{

class MessageParser
{
public:
    MessageParser() noexcept;
    ~MessageParser() noexcept;

    char const * getMessageType() const;
    char const * getMessageContent() const;

    void buildMessage(char const readCharacter);
    bool isMessageEnded();
    void reset();
    void run();

private:
    void parseMessageType();
    void parseMessageContent();

    static constexpr char MESSAGE_START = '$';
    static constexpr char MESSAGE_END = ';';
    static constexpr char CONTENT_DELIMITER = '=';

    static constexpr char CARRIAGE_RETURN = '\r';
    static constexpr char LINE_FEED = '\n';

    static constexpr size_t MAX_TYPE_SIZE = 8;
    static constexpr size_t MAX_CONTENT_SIZE = 64; /* TODO(m-peko): Support unlimited content size */

    String m_receivedMessage;
    bool m_messageStarted;
    bool m_messageEnded;
    char m_messageType[MAX_TYPE_SIZE];
    char m_messageContent[MAX_CONTENT_SIZE];
};

} /* SecureBLE */

#endif /* MessageParser_H */