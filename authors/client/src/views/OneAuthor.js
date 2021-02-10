import React, { useState, useEffect } from 'react'
import axios from 'axios';
import {Link, navigate} from '@reach/router';

const OneAuthor = props => {
    const [author, setAuthor] = useState({
        name: "Loading...",
    });
    

    useEffect(() => {
        axios.get(`http://localhost:8000/api/authors/${props.id}`)
            .then(response => {
                const fromServer = response.data.data;
                setAuthor(fromServer);
            });
    }, [])


    return (
        <div>
            <br></br>
            <Link to="/">Home</Link>
            <h2>Name: {author.name}</h2>
        </div>
    );
}

export default OneAuthor;