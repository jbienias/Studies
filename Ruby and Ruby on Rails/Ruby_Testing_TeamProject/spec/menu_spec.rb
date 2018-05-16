require_relative 'spec_helper'

describe Menu do
  let(:menu) { Menu.new }

  context '#initialize' do
    it 'initializes without error' do
      expect { menu.menu }.not_to raise_error
    end
    it 'raises error when initialized with wrong arguments' do
      expect { menu.menu(bad, thing, happend) }.to raise_error(NameError)
    end
    it 'creates new object without errors' do
      expect { menu.to be_an_instance_of(Menu) }
    end
  end
  context '#to_s' do
    it 'calls without errors' do
      expect { menu.to_s }.not_to raise_error
    end
    it 'returns a String type' do
      expect { menu.to_s.to be_a(String) }
    end
  end
  context '#clear' do
    it 'calls without errors' do
      expect { menu.clear }.not_to raise_error
    end
  end
end
