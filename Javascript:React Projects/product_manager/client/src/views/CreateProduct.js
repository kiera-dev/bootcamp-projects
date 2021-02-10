import React, { useState } from 'react'
import { Link, navigate } from '@reach/router';
import ProdForm from '../components/ProdForm';
import axios from 'axios';

const CreateProduct = () => {
    const [product, setProduct] = useState({
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
        setProduct({...product,[event.target.name]: event.target.value});
        // onchange validations
        const nameCheck = document.querySelector('#name-input');
        const priceCheck = document.querySelector('#price-input');
        const descCheck = document.querySelector('#desc-input');
        console.log(event.target.value.length);
        if (nameCheck.value.length < 2){
            document.getElementById("name-error").innerHTML = "error, <2 characters";
        }
        else
        {
            document.getElementById("name-error").innerHTML = "";
        }
        if (priceCheck.value < 0.01){
            document.getElementById("price-error").innerHTML = `Must be at least $0.01`;
        }
        else {
            document.getElementById("price-error").innerHTML = "";
        }
        if (descCheck.value.length < 5){
            document.getElementById("desc-error").innerHTML = "error, <5 characters";
        }
        else
        {
            document.getElementById("desc-error").innerHTML = "";
        }
    };

    const submitHandler = event => {
        event.preventDefault();
        axios.post('http://localhost:8000/api/products', product)
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
                <h1>Create Product</h1>
            </div>
            <div>
                <ProdForm 
                    changeHandler={ changeHandler }
                    submitHandler={ submitHandler }
                    product={ product }
                    errors={ errors }
                    action="Boop"/>
            </div>
        </div>
    );
}

export default CreateProduct;