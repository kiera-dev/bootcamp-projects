import './App.css';
import { Router, navigate } from '@reach/router';
import axios from 'axios';
import AllProducts from './views/AllProducts';
import OneProduct from './views/OneProduct';
import CreateProduct from './views/CreateProduct';
import EditProduct from './views/EditProduct';


function App() {
  return (
    <div className="App">
        <div className="container">
          <Router>
            <AllProducts path="/"/>
            <OneProduct path="/products/:id"/>
            <CreateProduct path="/products/new"/>
            <EditProduct path="/products/edit/:id"/>
          </Router>
        </div>
    </div>
  );
}

export default App;
