import React, { Component } from 'react';
import { HubConnectionBuilder } from '@aspnet/signalr/dist/esm';

class Chat extends Component {
    constructor(props) {
        super(props);
        this.state = {
            messages:[]
        };

        this.onAdd = this.onAdd.bind(this);
    }

    componentDidMount() {
       
        let connection = new HubConnectionBuilder()
            .withUrl("http://localhost:62319/chathub")
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
    }

    onAdd() {
        var data = { text: "fake", date: new Date() }
        var lstMsg = this.state.messages.concat(data);
        this.setState({ messages: lstMsg });
    }

    render() {
        return (
            <div>
                Chat Component {this.state.prop1}
                <button onClick={this.onAdd}>Add</button>
                <div>Qtd:{this.state.messages.length}</div>
                <div>
                    {this.state.messages.map((msg,idx) => 
                        <div key={idx}>
                            {msg.text}
                            <div><small>{msg.date.toString()}</small></div>
                        </div>
                    )}
                    </div>
            </div>
        );
    }
}

export default Chat;
