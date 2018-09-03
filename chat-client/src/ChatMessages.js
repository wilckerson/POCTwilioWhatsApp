import React, { Component } from 'react';
import moment from 'moment';

import './ChatMessages.css';

class ChatMessages extends Component {

    constructor(props) {
        super(props);

        this.scrollTo = this.scrollTo.bind(this);
    }
    
    hasMessages() {
        return this.props.messages && this.props.messages.length > 0;
    }

    scrollTo(elm) {

        if (elm) {
            this.bottomElm = elm;
        }

        if (this.bottomElm) {
            this.bottomElm.scrollIntoView({ behavior: "smooth" });
        }
    }

    render() {
        return (
            <div className="chat-list">
                
                {!this.hasMessages() && <div>Nenhuma mensagem</div>}
                {this.hasMessages() && this.props.messages.map((msg, idx) =>
                    <div key={idx} className={'chat-msg ' + (msg.mainUser ? 'main-user' : '')}>
                        <div className="user">{msg.mainUser ? "Você" : (msg.user || "Usuário desconhecido")}:</div>
                        <div className="body">{msg.text}</div>
                        <div className="date">{moment(msg.date).format("HH:mm")}</div>
                    </div>
                )}
                <div ref={(elm) => this.scrollTo(elm)}>&nbsp;</div>
            </div>
        );
    }
}

export default ChatMessages;
