import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr/dist/esm';

import ChatMessages from './ChatMessages';
import ChatSend from './ChatSend';

class Chat extends Component {
    constructor(props) {
        super(props);
        this.state = {
            messages: []
        };

        this.onSend = this.onSend.bind(this);
    }

    componentDidMount() {
       
        let connection = new HubConnectionBuilder()
            //.withUrl("http://localhost:62319/chathub")
            .withUrl("https://poc-twilio-whatsapp.azurewebsites.net/chathub")
            .build();

        var self = this;
        connection.on("ReceiveMessage", (data) => {
            console.log(data);
            var lstMsg = self.state.messages.concat(data);
            self.setState({ messages : lstMsg });
        });

        connection.start().catch(function (err) {
            alert("Error connecting the Hub")
            return console.error(err.toString());
        });

        window["hubConnection"] = connection;
        this.connection = connection;
    }

    onSend(msg) {
        //var data = { text: "fake", date: new Date(), mainUser:true }
        //var lstMsg = this.state.messages.concat(data);
        //this.setState({ messages: lstMsg });

        this.connection.invoke("SendMessage", "poc", msg);
    }

    render() {
        return (
            <div>
                <ChatMessages messages={this.state.messages} />
                <ChatSend onSend={this.onSend}/>
            </div>
        );
    }
}

export default Chat;
