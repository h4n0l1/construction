import { mount } from '@vue/test-utils';
import { describe, it, expect, vi } from 'vitest';
import FormComponent from '@/components/FormComponent.vue';

describe('FormComponent.vue', () => {
  it('renders form fields correctly', () => {
    const wrapper = mount(FormComponent, {
      props: {
        formData: {
          name: '',
          location: '',
          category: '',
          startDate: '',
          stage: null,
          details: ''
        },
        onSubmit: vi.fn()
      }
    });

    expect(wrapper.find('input[label="Name"]').exists()).toBe(true);
    expect(wrapper.find('input[label="Location"]').exists()).toBe(true);
    expect(wrapper.find('input[label="Category"]').exists()).toBe(true);
    expect(wrapper.find('input[label="Start Date"]').exists()).toBe(true);
    expect(wrapper.find('select[label="Stage"]').exists()).toBe(true);
    expect(wrapper.find('input[label="Details"]').exists()).toBe(true);
  });

  it('calls onSubmit with correct payload when form is valid', async () => {
    const formData = {
      name: 'Test Name',
      location: 'Test Location',
      category: 'Test Category',
      startDate: '2024-07-24',
      stage: 1,
      details: 'Test Details'
    };
    const onSubmit = vi.fn();
    const wrapper = mount(FormComponent, {
      props: {
        formData,
        onSubmit
      }
    });

    await wrapper.find('form').trigger('submit.prevent');
    expect(onSubmit).toHaveBeenCalledWith(formData);
  });
});
