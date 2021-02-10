import './App.css';
import React from "react";
import PokeList from './components/PokeList';
import Rotom from './images/479Rotom.png';
import axios from 'axios';

function App() {
  return (
    <div className="App">
      <div id="top">
        <h5>Alolan Pokedex!</h5>
      </div>

      <div id="container">
        <div className="half-container">
          <img src={Rotom}/>
        </div>
        <div className="half-container">
          <PokeList/>
        </div>
     
      </div>
      <footer className="mdl-mini-footer">
      <div className="mdl-mini-footer__left-section">
        <div className="mdl-logo">Alolan Pokdex ;)</div>
        <ul className="mdl-mini-footer__link-list">
          {/* <li><a href="#">Help</a></li>
          <li><a href="#">Privacy & Terms</a></li> */}
        </ul>
       </div>
      </footer>
    </div>
  );
}

export default App;
