import * as React from 'react';
import { Route } from 'react-router-dom';
import Layout from './components/Layout';
import Resources from './components/Resources';
 
import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Resources} />
    </Layout>
);
