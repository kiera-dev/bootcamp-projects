import {useState, useEffect} from "react";
import Search from './components/Search';
import './App.css';
import { Router } from '@reach/router';


function App() {
  const [searchTerms, setSearchTerms] = useState({
    category: "people",
    id: ""
  })

  const changeHandler = (event) =>{
    setSearchTerms({
      ...searchTerms,
      [event.target.name]: event.target.value
    })
  }

  const submitHandler = (event) =>{
    event.preventDefault();
    console.log(searchTerms);
  }

  const fetchData =()=> {
    const {category, id} = searchTerms
  }

  return (
    <div className="App">
      <div>
        <Search/>
      </div>
    </div>
  );
}

export default App;
