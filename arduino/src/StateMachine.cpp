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

#include <StateMachine.h>

StateMachine::StateMachine()
    : m_currentState(State::STATE_START)
{}

StateMachine::~StateMachine()
{}

void
StateMachine::onReceive(Event event)
{
    switch (event)
    {
    case Event::EVENT_CONNECT_REQ:
        if (State::STATE_START == m_currentState)
        {
            switchState(State::STATE_KEYS_GENERATION);
        }
        break;
    case Event::EVENT_PU_KEY_RECEIVED:
        if (State::STATE_KEYS_GENERATION == m_currentState)
        {
            switchState(State::STATE_SHARED_SECRET_GENERATION);
        }
        break;
    case Event::EVENT_SHARED_SECRET_SUCCESS:
        if (State::STATE_SHARED_SECRET_GENERATION == m_currentState)
        {
            switchState(State::STATE_ENCRYPTED_CONNECTION);
        }
        break;
    case Event::EVENT_SHARED_SECRET_FAILURE:
        if (State::STATE_SHARED_SECRET_GENERATION == m_currentState)
        {
            switchState(State::STATE_START);
        }
        break;
    case Event::EVENT_RESET:
        switchState(State::STATE_START);
        break;
    default:
        /* unknown event */
        break;
    }
}

void
StateMachine::switchState(State newState)
{
    m_currentState = newState;
    onEntry();
}

void
StateMachine::onEntry()
{
    switch (m_currentState)
    {
    case State::STATE_START:
        keyExchange.clearKeys();
        break;
    case State::STATE_KEYS_GENERATION:
        keyExchange.generateKeys();
        // send public key
        break;
    case State::STATE_SHARED_SECRET_GENERATION:
        // generate shared secret
        break;
    case State::STATE_ENCRYPTED_CONNECTION:
        break;
    default:
        /* unknown state */
        break;
    }
}
