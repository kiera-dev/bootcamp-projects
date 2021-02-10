import React, {useState, useEffect} from 'react'
import axios from 'axios';
import {Link, navigate} from '@reach/router';
import Form from '../components/Form';


const EditAuthor = props => {
    const {id} = props;
    const [author, setAuthor] = useState({
        name: "",

    });
    const [errors, setErrors] = useState({
        name: "",

    });

    const changeHandler = event => {
        setAuthor({...author,[event.target.name]: event.target.value});
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
        axios.patch(`http://localhost:8000/api/authors/${id}`, author)
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


    useEffect(() => {
        axios.get(`http://localhost:8000/api/authors/${id}`)
            .then(response => {
                console.log(response.data);
                const fromServer = response.data.data;
                if(response.data.message === "error" || fromServer === null ){
                    navigate("/");
                } else {
                    setAuthor(fromServer);
                }
            })
            .catch(err => console.log(err));
    }, [id])


    return (
        <div>
            <h1>Edit {author.name}</h1>
            <Link to="/">Home</Link>
            <div>
                <Form 
                    changeHandler={changeHandler}
                    submitHandler={submitHandler}
                    author={author}
                    errors={errors}
                    action="Beep"/>
            </div>
        </div>
    );
}

export default EditAuthor;