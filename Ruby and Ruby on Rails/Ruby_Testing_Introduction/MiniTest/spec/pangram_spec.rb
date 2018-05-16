require 'pangram'

describe '#pangram?' do
  it 'returns false if empty' do
    expect(Pangram.pangram?('')).to eq false
  end

  it 'returns false if string is not a pangram' do
    expect(Pangram.pangram?('abelar')).to eq false
  end

  it 'returns true if string is a pangram and mixedcase' do
    expect(Pangram.pangram?('the Quick brown fox Jumps over the lazy dog')).to eq true
  end

  it 'returns true if string is a pangram and only lowercase' do
    expect(Pangram.pangram?('the quick brown fox jumps over the lazy dog')).to eq true
  end

  it 'returns true if string is a pangram and only UPPERCASE' do
    expect(Pangram.pangram?('ABCDEFGHIJKLMNOPQRSTUVWXYZ')).to eq true
  end
end
