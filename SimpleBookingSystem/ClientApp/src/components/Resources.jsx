import axios from 'axios';
import React, { useEffect, useState } from 'react';
import 'react-toastify/dist/ReactToastify.css';
import { toast, ToastContainer } from 'react-toastify';
import Booking from './Booking';

function Resources() {
    const [isLoaded, setIsLoaded] = useState(false);
    const [error, setError] = useState(null);
    const [showBooking, setShowBooking] = useState(false);
    const [resources, setResources] = useState([]);
    const [selectedResource, setSelectedResource] = useState(null);


    const fetchResources = async () => {
        try {
            const res = await axios.get(`api/resources`);
            setIsLoaded(true);
            setResources(res.data);
        } catch (err) {
            setIsLoaded(true);
            setError(error);
        }
    };

    useEffect(() => {
        fetchResources();
    }, [])

    function handleBooking(e, resource) {
        setSelectedResource(resource);
        setShowBooking(true);
    }

    function handleResult(error){
        if (error){
            handleClose()
            toast.error(error.response.data, {
                position: toast.POSITION.TOP_CENTER
            });
        }
        else{
            handleClose()
            toast.success('Success', {
                position: toast.POSITION.TOP_CENTER
            });
        }
    }

    function handleClose(){
        setSelectedResource(null);
        setShowBooking(false);
    }

    function renderResourcesTable() {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {resources.map((resource) =>
                        <tr key={resource.id}>
                            <td>{resource.id}</td>
                            <td>{resource.name}</td>
                            <td><button onClick={(e) => handleBooking(e, resource)}>book here</button></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    if (error) {
        return <div>Error: {error.message}</div>;
    } else if (!isLoaded) {
        return <div>Loading...</div>;
    } else {
        return (
            <div>
                <p>Resources</p>
                {renderResourcesTable()}
                {showBooking && selectedResource &&
                    <Booking isOpen={showBooking} handleClose={handleClose} handleResult={handleResult} selectedResource={selectedResource}></Booking>
                }
                <ToastContainer />
            </div>
        );
    }
}

export default Resources;
