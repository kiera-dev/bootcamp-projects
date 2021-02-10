import React, {useState, useEffect} from "react";
import axios from "axios";
import { Link } from '@reach/router';



const AllProducts = () => {
    const [products, setProducts] = useState([]);


    useEffect(() => {
        axios.get('http://localhost:8000/api/products')
            .then(response => setProducts(response.data.data))
            .catch(err => console.log(err));
    }, [])


    const deleteHandler = i => {
        axios.delete(`http://localhost:8000/api/products/${products[i]._id}`)
            .then(response => {setProducts(products.filter((item,index) => index != i));})
            .catch(err => console.log(err));
    }


    return (
        <div>
            <div>
                <h1>All Products</h1>
                <div>
                    <Link to="/products/new">Add a Product</Link>
                </div>
            </div>
            <div>
                {
                    // i should change this to a table
                    products.map((item, i) => 
                    <p key={i}><Link to={`/products/${item._id}`}>{item.name}</Link> |   ${item.price} | <Link to={`/products/edit/${item._id}`}>Edit</Link> |   <button onClick={() => deleteHandler(i)}>Delete</button></p> )
                }
            </div> 
        </div>
    );
}

export default AllProducts;