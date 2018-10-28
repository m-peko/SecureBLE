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

#include <ECDHKeyExchange.h>

enum class State
{
    STATE_START,
    STATE_KEYS_GENERATION,
    STATE_SHARED_SECRET_GENERATION,
    STATE_ENCRYPTED_CONNECTION
};

enum class Event
{
    EVENT_CONNECT_REQ,
    EVENT_PU_KEY_RECEIVED,
    EVENT_SHARED_SECRET_SUCCESS,
    EVENT_SHARED_SECRET_FAILURE,
    EVENT_RESET
};

class StateMachine
{
public:
    StateMachine();
    ~StateMachine();

    void onReceive(Event event);
    void switchState(State newState);
    void onEntry();

private:
    State m_currentState;
    ECDHKeyExchange keyExchange;
};

#endif /* StateMachine_H */