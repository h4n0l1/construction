import { expect } from 'chai'
import { shallowMount } from '@vue/test-utils'
import Construction from '@/components/DataFetcher.vue'

describe('DataFetcher.vue', () => {
  it('renders props.msg when passed', () => {
    const msg = 'new message'
    const wrapper = shallowMount(Construction, {
      props: { msg }
    })
    expect(wrapper.text()).to.include(msg)
  })
})
