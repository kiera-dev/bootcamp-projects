import React, { useState, useEffect } from 'react'
import axios from 'axios';


const OneProduct = props => {
    const [product, setProduct] = useState({
        name: "Loading...",
        price: 0,
        desc: "loading" 
    });
    

    useEffect(() => {
        axios.get(`http://localhost:8000/api/products/${props.id}`)
            .then(response => {
                const fromServer = response.data.data;
                setProduct(fromServer);
            });
    }, [])


    return (
        <div>
            <br></br>
            <h2>Name: {product.name}</h2>
            <p>Price: ${product.price}</p>
            <p>Description: {product.desc}</p>
        </div>
    );
}

export default OneProduct