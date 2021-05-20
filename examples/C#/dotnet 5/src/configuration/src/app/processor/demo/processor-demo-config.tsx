import {
    Spinner, SpinnerSize, Stack, TextField
} from '@fluentui/react';
import React, {
    useEffect,
    useState
} from 'react';
import { useTranslation } from 'react-i18next';

export interface IProcessorDemoConfig {
    Config1: string;
    Config2: string;
    Config3: string;

    errors?: { [id: string] : string; };
}

export const ProcessorDemoConfig: React.FunctionComponent = () => {
    const { t: translate } = useTranslation('module');

    const stillLoading: boolean = true;
    const initState = initialize();

    const [stillLoadingState, setStillLoading] = useState(stillLoading);

    const [config1, setConfig1] = useState(initState.Config1);
    const [config2, setConfig2] = useState(initState.Config2);
    const [config3, setConfig3] = useState(initState.Config3);

    const [errors, setErrors] = useState(initState.errors);

    useEffect(() => {
        (window as any).saveProcessorDemoConfig = saveConfig.bind(this);
    });

    useEffect(() => {
        if (stillLoadingState === true) {
            loadConfig()
                .then((itemConfig) => {

                })
                .catch((error) => {
                    // Handle error here
                })
                .finally(() => {
                    setStillLoading(false);
                });
        }
    }, [stillLoadingState]);

    useEffect(() => {
        const config : IProcessorDemoConfig = {
            Config1: config1,
            Config2: config2,
            Config3: config3
        };

        function validate(config: IProcessorDemoConfig) {
            const errors: { [id: string] : string; } = {
                config1: '',
                config2: '',
                config3: ''
            };

            if (!config.Config1 || config.Config1.length === 0) {
                errors.config1 = translate('text.VALIDATE_CONFIG_1_NO_NAME', 'You must specify a name for the config 1');
            }

            if (!config.Config2 || config.Config2.length === 0) {
                errors.config2 = translate('text.VALIDATE_CONFIG_2_NO_NAME', 'You must specify a name for the config 2');
            }

            if (!config.Config3 || config.Config3.length === 0) {
                errors.config3 = translate('text.VALIDATE_CONFIG_3_NO_NAME', 'You must specify a name for the config 3');
            }

            return errors;
        }

        setErrors(validate(config));
    }, [config1, config2, config3, translate]);

    function initialize() : IProcessorDemoConfig {
        return {
            Config1: '',
            Config2: '',
            Config3: ''
        };
    }

    async function loadConfig() : Promise<IProcessorDemoConfig> {
        // Define new config
        const itemConfig : IProcessorDemoConfig = {
            Config1: '',
            Config2: '',
            Config3: ''
        };

        // Load configuration for the processor
        const config = await (window as any).sdkUiEvents?.loadConfig();

        if (config !== undefined
            && config !== null
            && config.parameters !== undefined
            && config.parameters !== null) {
            itemConfig.Config1 = config.parmeters.config_1;
            itemConfig.Config2 = config.parmeters.config_2;
            itemConfig.Config3 = config.parmeters.config_3;
        }

        return itemConfig;
    }

    function saveConfig() {
        // Define config item for execution
        const parameter: { [id: string] : string; } = {};

        parameter.config_1 = config1;
        parameter.config_2 = config2;
        parameter.config_3 = config3;

        return {
            parameters: parameter
        };
    }

    if (stillLoading === true) {
        return (
            <Stack verticalFill={true} verticalAlign='center' horizontalAlign='center'>
                <Spinner size={SpinnerSize.large} />
            </Stack>
        );
    }

    return (
        <Stack tokens={{
            childrenGap: '1.25rem'
        }} styles={{
            root: {
                padding: '1.25rem',
                ' > div': {
                    display: 'flex',
                    flexFlow: 'column nowrap',
                    boxSizing: 'border-box',
                    height: '100%'
                }
            }
        }}>
            <TextField
                label={translate('text.LABEL_CONFIG_1', 'Config 1')}
                required
                value={config1}
                errorMessage={errors?.config1}
                onChange={(e, value) => { return setConfig1(value || ''); }
                }
            ></TextField>

            <TextField
                label={translate('text.LABEL_CONFIG_2', 'Config 2')}
                required
                value={config2}
                errorMessage={errors?.config2}
                onChange={(e, value) => { return setConfig2(value || ''); }
                }
            ></TextField>

            <TextField
                label={translate('text.LABEL_CONFIG_3', 'Config 3')}
                required
                value={config3}
                errorMessage={errors?.config3}
                onChange={(e, value) => { return setConfig3(value || ''); }
                }
            ></TextField>
        </Stack>
    );
};
