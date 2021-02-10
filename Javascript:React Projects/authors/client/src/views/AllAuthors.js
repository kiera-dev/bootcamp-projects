import React, {useState, useEffect} from "react";
import axios from "axios";
import { Link } from '@reach/router';



const AllAuthors = () => {
    const [authors, setAuthors] = useState([]);


    useEffect(() => {
        axios.get('http://localhost:8000/api/authors')
            .then(response => setAuthors(response.data.data))
            .catch(err => console.log(err));
    }, [])


    const deleteHandler = i => {
        axios.delete(`http://localhost:8000/api/authors/${authors[i]._id}`)
            .then(response => {setAuthors(authors.filter((item,index) => index != i));})
            .catch(err => console.log(err));
    }


    return (
        <div>
            <div>
                <h1>All Authors</h1>
                <div>
                    <Link to="/authors/new">Add author</Link>
                </div>
            </div>
            <div>
            
                     
                    
                    <table className="mdl-data-table mdl-js-data-table">
                        <thead>
                            <tr>
                                <th className="mdl-data-table__cell--non-numeric">Name</th>
                                <th className="mdl-data-table__cell--non-numeric">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                authors.map((item, i) =>
                            <tr>
                                <td p key={i} className="mdl-data-table__cell--non-numeric"><Link to={`/authors/${item._id}`}>{item.name}</Link></td>
                                <td p key={i} className="mdl-data-table__cell--non-numeric"><Link to={`/authors/edit/${item._id}`}>Edit</Link> <button onClick={() => deleteHandler(i)}>Delete</button> </td>
                            </tr>
                            )}
                        </tbody>
                    </table>
                
                
            </div> 
        </div>
    );
}

export default AllAuthors;