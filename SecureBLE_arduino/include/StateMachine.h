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

#ifndef StateMachine_H
#define StateMachine_H

#include <SoftwareSerial.h>
#include <ECDHKeyExchange.h>

namespace SecureBLE
{

enum class State
{
    STATE_UNKNOWN,
    STATE_START,
    STATE_KEYS_GENERATION,
    STATE_SHARED_SECRET_GENERATION,
    STATE_ENCRYPTED_CONNECTION
};

enum class Event
{
    EVENT_UNKNOWN,
    EVENT_CONNECT_REQ,
    EVENT_PU_KEY_RECEIVED,
    EVENT_SHARED_SECRET_SUCCESS,
    EVENT_SHARED_SECRET_FAILURE,
    EVENT_RESET
};

class StateMachine
{
public:
    StateMachine(SoftwareSerial const& bleModule);
    ~StateMachine() noexcept;

    void onReceive(char const *messageType, char const *messageContent);

private:
    Event messageTypeToEvent(char const *messageType);

    void switchState(State newState);
    void onEntry();

    /* message types */
    static const char CONNECT_MESSAGE_TYPE[];
    static const char PU_MESSAGE_TYPE[];
    static const char SUCCESS_MESSAGE_TYPE[];
    static const char FAILURE_MESSAGE_TYPE[];
    static const char RESET_MESSAGE_TYPE[];

    State m_currentState;
    SoftwareSerial m_bleModule;
    ECDHKeyExchange m_keyExchange;
};

} /* SecureBLE */

#endif /* StateMachine_H */
