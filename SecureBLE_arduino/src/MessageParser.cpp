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

#include <MessageParser.h>

namespace SecureBLE
{

MessageParser::MessageParser()
    : m_receivedMessage(""),
      m_messageStarted(false),
      m_messageEnded(false)
{}

MessageParser::~MessageParser()
{}

char const *
MessageParser::getMessageType() const
{
    return m_messageType;
}

char const *
MessageParser::getMessageContent() const
{
    return m_messageContent;
}

void
MessageParser::buildMessage(char const character)
{
    if (MESSAGE_START == character)
    {
        m_messageStarted = true;
    }

    if (m_messageStarted &&
        CARRIAGE_RETURN != character &&
        LINE_FEED != character)
    {
        m_receivedMessage.concat(character);

        if (MESSAGE_END == character)
        {
            m_messageEnded = true;
        }
    }
}

bool
MessageParser::isMessageEnded()
{
    return m_messageEnded;
}

void
MessageParser::reset()
{
    m_receivedMessage = "";
    m_messageStarted = false;
    m_messageEnded = false;
    memset(m_messageType, 0, MAX_TYPE_SIZE);
    memset(m_messageContent, 0, MAX_CONTENT_SIZE);
}

void
MessageParser::run()
{
    parseMessageType();
    parseMessageContent();
}

void
MessageParser::parseMessageType()
{
    size_t messageIdx = 0;
    size_t typeIdx = 0;

    while (CONTENT_DELIMITER != m_receivedMessage[messageIdx] &&
           MESSAGE_END != m_receivedMessage[messageIdx])
    {
        if (MESSAGE_START != m_receivedMessage[messageIdx])
        {
            m_messageType[typeIdx] = m_receivedMessage[messageIdx];
            typeIdx++;
        }
        messageIdx++;
    }
}

void
MessageParser::parseMessageContent()
{
    size_t messageIdx = 0;
    size_t contentIdx = 0;

    while (CONTENT_DELIMITER != m_receivedMessage[messageIdx] &&
           MESSAGE_END != m_receivedMessage[messageIdx])
    {
        messageIdx++;
    }

    if (MESSAGE_END == m_receivedMessage[messageIdx])
    {
        /* no value in message */
        return;
    }

    messageIdx++;
    while (MESSAGE_END != m_receivedMessage[messageIdx])
    {
        m_messageContent[contentIdx] = m_receivedMessage[messageIdx];
        messageIdx++;
        contentIdx++;
    }
}

} /* SecureBLE */
