import React, { Component } from 'react';

class ChatSend extends Component {

    constructor(props) {
        super(props);

        this.state = {
            value: ''
        }

        this.onClickSend = this.onClickSend.bind(this);
        this.handleChange = this.handleChange.bind(this);
    } 

    onClickSend() {
        var msg = this.state.value;
        if (msg) {
            this.setState({ value: '' });            
            this.props.onSend && this.props.onSend(msg);
        }
    }

    handleChange(event) {
        this.setState({ value: event.target.value });
    }

    render() {
        return (
            <div>
                <textarea onChange={this.handleChange} value={this.state.value}></textarea>
                <div>
                    <button onClick={this.onClickSend} disabled={!this.state.value}>Enviar</button>
                </div>
            </div>
        );
    }
}

export default ChatSend;
