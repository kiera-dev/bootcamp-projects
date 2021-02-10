import React, { useState } from 'react'
import { Link, navigate } from '@reach/router';
import Form from '../components/Form';
import axios from 'axios';

const CreateAuthor = () => {
    const [author, setAuthor] = useState({
        name: "",
        price: "",
        desc: ""
    });
    const [errors, setErrors] = useState({
        name: "",
        price: "",
        desc: ""
    })


    const changeHandler = event => {
        setAuthor({...author,[event.target.name]: event.target.value});
        // onchange validations
        const nameCheck = document.querySelector('#name-input');

        if (nameCheck.value.length < 3){
            document.getElementById("name-error").innerHTML = "error, <3 characters";
        }
        else
        {
            document.getElementById("name-error").innerHTML = "";
        }
    };

    const submitHandler = event => {
        event.preventDefault();
        axios.post('http://localhost:8000/api/authors', author)
            .then(response => {
                const res = response.data;
                if(res.message == "error") {
                    setErrors(res.data.errors);
                } else {
                    navigate("/");
                }
            })
            .catch(err => console.log(err));
    }

    return (
        <div>
            <div>
                <h1>Create Author</h1>
                <Link to="/">Home</Link>
            </div>
            <div>
                <Form 
                    changeHandler={ changeHandler }
                    submitHandler={ submitHandler }
                    author={ author }
                    errors={ errors }
                    action="Boop"/>
            </div>
        </div>
    );
}

export default CreateAuthor;