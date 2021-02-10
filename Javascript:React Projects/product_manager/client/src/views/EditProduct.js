import React, {useState, useEffect} from 'react'
import axios from 'axios';
import {Link, navigate} from '@reach/router';
import ProdForm from '../components/ProdForm';


const EditProduct = props => {
    const {id} = props;
    const [product, setProduct] = useState({
        name: "",
        age: ""
    });
    const [errors, setErrors] = useState({
        name: "",
        age: ""
    });

    const changeHandler = event => {
        setProduct({...product,[event.target.name]: event.target.value});
    };

    const submitHandler = event => {
        event.preventDefault();
        axios.patch(`http://localhost:8000/api/products/${id}`, product)
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
        axios.get(`http://localhost:8000/api/products/${id}`)
            .then(response => {
                console.log(response.data);
                const fromServer = response.data.data;
                if(response.data.message === "error" || fromServer === null ){
                    navigate("/");
                } else {
                    setProduct(fromServer);
                }
            })
            .catch(err => console.log(err));
    }, [id])


    return (
        <div>
            <h1>Edit {product.name}</h1>
            <div>
                <ProdForm 
                    changeHandler={changeHandler}
                    submitHandler={submitHandler}
                    product={product}
                    errors={errors}
                    action="Beep"/>
            </div>
        </div>
    );
}

export default EditProduct