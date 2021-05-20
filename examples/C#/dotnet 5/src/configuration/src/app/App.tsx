import {
    Spinner, SpinnerSize, Stack
} from '@fluentui/react';
import React, { Suspense } from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route
} from 'react-router-dom';

export const App: React.FunctionComponent = () => {
    return (
        <Stack verticalFill>
            <Router>
                <Switch>
                    <Route path='*'>
                        <Suspense fallback={<Stack verticalFill verticalAlign='center'><Spinner size={SpinnerSize.large}></Spinner></Stack>}>
                            <Switch>
                                <Route path='/api/v1/processor/demo/config'>
                                    <div></div>
                                </Route>
                            </Switch>
                        </Suspense>
                    </Route>
                </Switch>
            </Router>
        </Stack>
    );
};
