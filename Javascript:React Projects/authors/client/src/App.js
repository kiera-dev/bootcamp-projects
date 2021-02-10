import './App.css';
import { Router, navigate } from '@reach/router';
import axios from 'axios';
import AllAuthors from './views/AllAuthors';
import OneAuthor from './views/OneAuthor';
import CreateAuthor from './views/CreateAuthor';
import EditAuthor from './views/EditAuthor';


function App() {
  return (
    <div className="App">
        <div className="container">
          <Router>
            <AllAuthors path="/"/>
            <CreateAuthor path="/authors/new"/>
            <EditAuthor path="/authors/edit/:id"/>
          </Router>
        </div>
    </div>
  );
}

export default App;
