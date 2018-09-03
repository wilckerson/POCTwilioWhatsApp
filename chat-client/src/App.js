import React, { Component } from 'react';
import Chat from './Chat';
import logo from './logo.svg';
import './App.css';

class App extends Component {
  render() {
    return (
      <div className="App">
            <h1>POCTwilioWhatsApp</h1>
            <Chat></Chat>
      </div>
    );
  }
}

export default App;
